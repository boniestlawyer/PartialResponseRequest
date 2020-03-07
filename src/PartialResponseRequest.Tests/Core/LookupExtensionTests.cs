using FluentAssertions;
using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Core.TokenReaders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PartialResponseRequest.Tests.Core
{
    public class LookupExtensionTests
    {
        [Fact]
        public void EnsureExpectedChar_ThrowsIfInvalidCharFound()
        {
            var enumerator = new LookupEnumerator<char>("abc".GetEnumerator());
            enumerator.MoveNext();
            new Action(() => enumerator.EnsureExpectedChar(new HashSet<char>(new[] { 'a' })))
                .Should().Throw<UnexpectedCharException>();
        }

        [Fact]
        public void EnsureExpectedChar_DoesNotThrowForValidChars()
        {
            var enumerator = new LookupEnumerator<char>("abc".GetEnumerator());
            new Action(() => enumerator.EnsureExpectedChar(new HashSet<char>(new[] { 'b' })))
                .Should().NotThrow<UnexpectedCharException>();
        }

        [Fact]
        public void IfNext_ReturnsIfFinishedEnumerator_DoesntCallAnyCallbacks()
        {
            var enumerator = new LookupEnumerator<char>("a".GetEnumerator());
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.IfNext('a', (c) => Assert.True(false), (c) => Assert.True(false));
        }
    }
}
