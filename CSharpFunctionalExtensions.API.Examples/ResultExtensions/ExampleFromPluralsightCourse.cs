namespace CSharpFunctionalExtensions.API.Examples.ResultExtensions {
    public class ExampleFromPluralsightCourse {
        public string Promote(long id) {
            var gateway = new EmailGateway();

            return GetById(id)
                .ToResult(new Error(ErrorCode.NOT_FOUND, "Customer with such Id is not found: " + id))
                .Ensure(customer => customer.CanBePromoted(), new Error(ErrorCode.FAILED_PRECONDITION, "The customer has the highest status possible"))
                .Tap(customer => customer.Promote())
                .Bind(customer => gateway.SendPromotionNotification(customer.Email))
                .Finally(result => result.IsSuccess ? "Ok" : result.Error.Message);
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
        }

        public class EmailGateway {
            public Result SendPromotionNotification(string email) {
                return Result.Success();
            }
        }
    }
}