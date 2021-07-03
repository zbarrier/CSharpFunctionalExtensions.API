using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests.Extensions {
    public abstract class CheckTestsBase : TestBase {
        protected bool actionExecuted;

        protected Result GetResult(bool isSuccess) {
            actionExecuted = true;
            return isSuccess
                ? Result.Success()
                : FailedResult;
        }

        protected Func<T, Result<K>> Func_Result_K(bool isSuccess) {
            return isSuccess
                ? new Func<T, Result<K>>(t => {
                    actionExecuted = true;
                    return Result.Success(K.Value);
                })
                : t => {
                    actionExecuted = true;
                    return FailedResultK;
                };
        }

        protected Func<T, Task<Result<K>>> Func_Task_Result_K(bool isSuccess) {
            return isSuccess
                ? new Func<T, Task<Result<K>>>(t => {
                    actionExecuted = true;
                    return Result.Success(K.Value).AsTask();
                })
                : t => {
                    actionExecuted = true;
                    return FailedResultK.AsTask();
                };
        }

        protected Result<T> FailedResultT => Result.Failure<T>(ErrorMessage);
        protected Result<K> FailedResultK => Result.Failure<K>(ErrorMessage);
        protected Result FailedResult => Result.Failure(ErrorMessage);
    }
}