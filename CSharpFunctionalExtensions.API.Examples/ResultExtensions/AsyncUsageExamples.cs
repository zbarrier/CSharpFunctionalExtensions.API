using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API.Examples.ResultExtensions {
    public class AsyncUsageExamples {
        public async Task<string> Promote_with_async_methods_in_the_beginning_of_the_chain(long id) {
            var gateway = new EmailGateway();

            return await GetByIdAsync(id)
                .ToResult(new Error(ErrorCode.NOT_FOUND, "Customer with such Id is not found: " + id))
                .Ensure(customer => customer.CanBePromoted(), new Error(ErrorCode.FAILED_PRECONDITION, "The customer has the highest status possible"))
                .Tap(customer => customer.Promote())
                .Bind(customer => gateway.SendPromotionNotification(customer.Email))
                .Finally(result => result.IsSuccess ? "Ok" : result.Error.Message);
        }

        public async Task<string> Promote_with_async_methods_in_the_beginning_and_in_the_middle_of_the_chain(long id) {
            var gateway = new EmailGateway();

            return await GetByIdAsync(id)
                .ToResult(new Error(ErrorCode.NOT_FOUND, "Customer with such Id is not found: " + id))
                .Ensure(customer => customer.CanBePromoted(), new Error(ErrorCode.FAILED_PRECONDITION, "The customer has the highest status possible"))
                .Tap(customer => customer.PromoteAsync())
                .Bind(customer => gateway.SendPromotionNotificationAsync(customer.Email))
                .Finally(result => result.IsSuccess ? "Ok" : result.Error.Message);
        }

        public async Task<string> Promote_with_async_methods_in_the_beginning_and_in_the_middle_of_the_chain_using_compensate(long id) {
            var gateway = new EmailGateway();

            return await GetByIdAsync(id)
                .ToResult(new Error(ErrorCode.NOT_FOUND, "Customer with such Id is not found: " + id))
                .Ensure(customer => customer.CanBePromoted(), new Error(ErrorCode.FAILED_PRECONDITION, "Need to ask manager"))
                .OnFailure(error => Log(error.Message))
                .OnFailureCompensate(() => AskManager(id))
                .Tap(customer => Log("Manager approved promotion"))
                .Tap(customer => customer.PromoteAsync())
                .Bind(customer => gateway.SendPromotionNotificationAsync(customer.Email))
                .Finally(result => result.IsSuccess ? "Ok" : result.Error.Message);
        }

        void Log(string message) {
        }

        Task<Result<Customer>> AskManager(long id) {
            return Task.FromResult(Result.Success(new Customer()));
        }

        public Task<Maybe<Customer>> GetByIdAsync(long id) {
            return Task.FromResult((Maybe<Customer>)new Customer());
        }

        public Maybe<Customer> GetById(long id) {
            return new Customer();
        }

        public class Customer {
            public string Email { get; }

            public Customer() {
            }

            public bool CanBePromoted() {
                return true;
            }

            public void Promote() {
            }

            public Task PromoteAsync() {
                return Task.FromResult(1);
            }
        }

        public class EmailGateway {
            public Result SendPromotionNotification(string email) {
                return Result.Success();
            }

            public Task<Result> SendPromotionNotificationAsync(string email) {
                return Task.FromResult(Result.Success());
            }
        }
    }
}
