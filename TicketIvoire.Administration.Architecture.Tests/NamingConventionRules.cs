using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Application.Queries;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.BusinessRules;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Architecture.Tests;

public class NamingConventionRules
{
    [Fact]
    public void EntityTypeConfigurations_ShouldEndWith_EntityTypeConfiguration()
    {
        var result = Types.InAssembly(typeof(Infrastructure.ServicesExtensions).Assembly)
            .That()
            .ImplementInterface(typeof(IEntityTypeConfiguration<>))
            .Should()
            .HaveNameEndingWith("EntityTypeConfiguration", StringComparison.Ordinal)
            .GetResult();
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void DbContexts_ShouldEndWith_Context()
    {
        var result = Types.InAssembly(typeof(Infrastructure.ServicesExtensions).Assembly)
            .That()
            .AreClasses()
            .And()
            .Inherit(typeof(DbContext))
            .Should()
            .ResideInNamespace("TicketIvoire.Administration.Infrastructure.Persistence")
            .And()
            .HaveNameEndingWith("Context", StringComparison.Ordinal)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Commands_ShouldEndWith_Command()
    {
        var result = Types.InAssembly(typeof(Application.ServicesExtensions).Assembly)
            .That()
            .ImplementInterface(typeof(ICommand))
            .Or()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .HaveNameEndingWith("Command", StringComparison.Ordinal)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void CommandHandlers_ShouldEndWith_CommandHandler()
    {
        var result = Types.InAssembly(typeof(Application.ServicesExtensions).Assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should()
            .HaveNameEndingWith("CommandHandler", StringComparison.Ordinal)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Queries_ShouldEndWith_Query()
    {
        var result = Types.InAssembly(typeof(Application.ServicesExtensions).Assembly)
            .That()
            .ImplementInterface(typeof(IQuery<>))
            .Should()
            .HaveNameEndingWith("Query", StringComparison.Ordinal)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void QueryHandlers_ShouldEndWith_QueryHandler()
    {
        var result = Types.InAssembly(typeof(Application.ServicesExtensions).Assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler", StringComparison.Ordinal)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void CommandValidators_ShouldEndWith_Validator()
    {
        var result = Types.InAssembly(typeof(Application.ServicesExtensions).Assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IValidator<>))
            .Should()
            .HaveNameEndingWith("Validator", StringComparison.Ordinal)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void BusinessRules_ShouldEndWith_Rule()
    {
        var result = Types.InAssembly(typeof(Domain.PropositionEvenements.PropositionEvenement).Assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IBusinessRule))
            .Should()
            .HaveNameEndingWith("Rule", StringComparison.Ordinal)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void DomainEvents_ShouldEndWith_Event()
    {
        var result = Types.InAssembly(typeof(Domain.PropositionEvenements.PropositionEvenement).Assembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("Event", StringComparison.Ordinal)
            .GetResult();
            
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void TypeIds_ShouldEndWith_Id() 
    {
        var result = Types.InAssembly(typeof(Domain.PropositionEvenements.PropositionEvenement).Assembly)
            .That()
            .Inherit(typeof(TypedIdBase))
            .Should()
            .HaveNameEndingWith("Id", StringComparison.Ordinal)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
