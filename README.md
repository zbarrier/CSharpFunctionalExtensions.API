Functional Extensions for C#
======================================================
This library is an opinated version of the CSharpFunctionalExtensions for APIs.

This library helps write code in more functional way for APIs. To get to know more about the principles behind the orginal CSharpFunctionalExtensions which this is based off of, check out the [Applying Functional Principles in C# Pluralsight course](https://enterprisecraftsmanship.com/ps-func).

## Installation

Available on [nuget](https://www.nuget.org/packages/CSharpFunctionalExtensions.API/)

	PM> Install-Package CSharpFunctionalExtensions.API

## Remaining of README is from original CSharpFunctionalExtensions

## Testing

For extension methods on top of this library's `Result` and `Maybe` that you can use in tests, see [this nuget package](https://www.nuget.org/packages/FluentAssertions.CSharpFunctionalExtensions/) (GitHub link: https://github.com/pedromtcosta/FluentAssertions.CSharpFunctionalExtensions).

Example:

```csharp
// Arrange
var myClass = new MyClass();

// Act
Result result = myClass.TheMethod();

// Assert
result.Should().BeSuccess();
```

## Get rid of primitive obsession

```csharp
Result<CustomerName> name = CustomerName.Create(model.Name);
Result<Email> email = Email.Create(model.PrimaryEmail);

Result result = Result.Combine(name, email);
if (result.IsFailure)
    return Error(result.Error);

var customer = new Customer(name.Value, email.Value);
```

## Make nulls explicit with the Maybe type

```csharp
Maybe<Customer> customerOrNothing = _customerRepository.GetById(id);
if (customerOrNothing.HasNoValue)
    return Error("Customer with such Id is not found: " + id);
```

## Compose multiple operations in a single chain

```csharp
return _customerRepository.GetById(id)
    .ToResult("Customer with such Id is not found: " + id)
    .Ensure(customer => customer.CanBePromoted(), "The customer has the highest status possible")
    .Tap(customer => customer.Promote())
    .Tap(customer => _emailGateway.SendPromotionNotification(customer.PrimaryEmail, customer.Status))
    .Finally(result => result.IsSuccess ? Ok() : Error(result.Error));
```

## Wrap multiple operations in a TransactionScope

```csharp
return _customerRepository.GetById(id)
    .ToResult("Customer with such Id is not found: " + id)
    .Ensure(customer => customer.CanBePromoted(), "The customer has the highest status possible")
    .WithTransactionScope(customer => Result.Success(customer)
        .Tap(customer => customer.Promote())
        .Tap(customer => customer.ClearAppointments()))
    .Tap(customer => _emailGateway.SendPromotionNotification(customer.PrimaryEmail, customer.Status))
    .Finally(result => result.IsSuccess ? Ok() : Error(result.Error));
```

## Readings and watchings

 * [Functional C#: Primitive obsession](http://enterprisecraftsmanship.com/2015/03/07/functional-c-primitive-obsession/)
 * [Functional C#: Non-nullable reference types](http://enterprisecraftsmanship.com/2015/03/13/functional-c-non-nullable-reference-types/)
 * [Functional C#: Handling failures, input errors](http://enterprisecraftsmanship.com/2015/03/20/functional-c-handling-failures-input-errors/)
 * [Applying Functional Principles in C# Pluralsight course](https://enterprisecraftsmanship.com/ps-func)
