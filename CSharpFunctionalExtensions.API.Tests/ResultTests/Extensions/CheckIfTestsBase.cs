using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests.Extensions {
    public abstract class CheckIfTestsBase : TestBase {
        protected bool actionExecuted;
        protected bool predicateExecuted;

        protected CheckIfTestsBase() {
            actionExecuted = false;
            predicateExecuted = false;
        }

        protected Result Func_Result(bool _) { actionExecuted = true; return Result.Success(); }

        protected Result<K> Func_Result_K(bool _) { actionExecuted = true; return Result.Success<K>(K.Value); }

        protected Task<Result> Task_Func_Result(bool _) { actionExecuted = true; return Result.Success().AsTask(); }
        protected Task<Result<K>> Task_Func_Result_K(bool _) { actionExecuted = true; return Result.Success<K>(K.Value).AsTask(); }

        protected bool Predicate(bool b) { predicateExecuted = true; return b; }
    }
}