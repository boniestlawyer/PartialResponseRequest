using FluentAssertions;
using Moq;
using PartialResponseRequest.Fields.Interpreters;
using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System.Collections.Generic;
using Xunit;

namespace PartialResponseRequest.Tests.Interpreters
{
    public class Model
    {
        public string Name { get; set; }
        public List<Model> List { get; set; }
    }

    public class FieldsQueryInterpreterTests
    {
        [Fact]
        public void Includes_True_IfNoFieldTokensGiven()
        {
            FieldsQueryInterpreter interpreter = new FieldsQueryInterpreter();
            interpreter.Includes("any-field").Should().BeTrue();
        }

        [Fact]
        public void Includes_True_IfStartIsOneOfTheFields()
        {
            FieldsQueryInterpreter interpreter = new FieldsQueryInterpreter(new[] { new FieldToken("other-field", new List<ParameterToken>(), new List<FieldToken>()), new FieldToken("*", new List<ParameterToken>(), new List<FieldToken>()) });
            interpreter.Includes("any-field").Should().BeTrue();
        }

        [Fact]
        public void Includes_False_IfFieldIsNotPresent()
        {
            FieldsQueryInterpreter interpreter = new FieldsQueryInterpreter(new[] { new FieldToken("other-field", new List<ParameterToken>(), new List<FieldToken>()) });
            interpreter.Includes("any-field").Should().BeFalse();
        }

        [Fact]
        public void Visit_ReturnsEverythingIncluded_IfNoFiltersPresent()
        {
            FieldsQueryInterpreter interpreter = new FieldsQueryInterpreter();
            interpreter.Visit("any-field").Should().BeOfType<EverythingIncluded>();
        }

        [Fact]
        public void Visit_ReturnsEverythingIncluded_IfStarFilterIncluded()
        {
            FieldsQueryInterpreter interpreter = new FieldsQueryInterpreter(new[] { new FieldToken("other-field", new List<ParameterToken>(), new List<FieldToken>()), new FieldToken("*", new List<ParameterToken>(), new List<FieldToken>()) });
            interpreter.Visit("any-field").Should().BeOfType<EverythingIncluded>();
        }

        [Fact]
        public void Visit_ReturnsNothingIncluded_IfFieldIsNotIncluded()
        {
            FieldsQueryInterpreter interpreter = new FieldsQueryInterpreter(new[] { new FieldToken("other-field", new List<ParameterToken>(), new List<FieldToken>()) });
            interpreter.Visit("any-field").Should().BeOfType<NothingIncluded>();
        }

        [Fact]
        public void Visit_ReturnsNewNestedQueryInterpreter_IfFieldIsIncluded()
        {
            FieldsQueryInterpreter interpreter = new FieldsQueryInterpreter(new[] {
                new FieldToken("other-field", new List<ParameterToken>(), new List<FieldToken>() {
                    new FieldToken("id", new List<ParameterToken>(), new List<FieldToken>()) }
                ) }
            );

            IFieldsQueryInterpreter nested = interpreter.Visit("other-field");
            nested.Should().BeOfType<FieldsQueryInterpreter>();
            nested.Includes("id").Should().BeTrue();
            nested.Includes("other").Should().BeFalse();
        }

        [Fact]
        public void StrongTypedVersion_Visit_PassesToBaseInterpreterCorrectly()
        {
            Mock<IFieldsQueryInterpreter> mock = new Mock<IFieldsQueryInterpreter>();
            var interpreter = new FieldsQueryInterpreter<Model>(mock.Object);
            interpreter.Visit(x => x.Name);

            mock.Verify(x => x.Visit("name"));
        }

        [Fact]
        public void StrongTypedVersion_VisitList_PassesToBaseInterpreterCorrectly()
        {
            Mock<IFieldsQueryInterpreter> mock = new Mock<IFieldsQueryInterpreter>();
            var interpreter = new FieldsQueryInterpreter<Model>(mock.Object);
            interpreter.VisitList(x => x.List);

            mock.Verify(x => x.Visit("list"));
        }

        [Fact]
        public void StrongTypedVersion_Includes_PassesToBaseInterpreterCorrectly()
        {
            Mock<IFieldsQueryInterpreter> mock = new Mock<IFieldsQueryInterpreter>();
            var interpreter = new FieldsQueryInterpreter<Model>(mock.Object);
            interpreter.Includes(x => x.Name);

            mock.Verify(x => x.Includes("name"));
        }
    }
}
