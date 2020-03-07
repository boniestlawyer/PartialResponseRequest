using FluentAssertions;
using Moq;
using PartialResponse.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PartialResponse.Tests.Core
{
    public class LookupEnumeratorTests
    {
        private LookupEnumerator<int> enumerator;

        public LookupEnumeratorTests()
        {
            enumerator = new LookupEnumerator<int>(new int[] { 1, 2, 3 }.Cast<int>().GetEnumerator());
        }

        [Fact]
        public void GetPrevious_ThrowsIfFirst()
        {
            new Action(() => enumerator.GetPrevious())
                .Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void GetPrevious_ReturnsPrevious()
        {
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.GetPrevious().Should().Be(1);
        }

        [Fact]
        public void GetNext_ThrowsAtFirst()
        {
            new Action(() => enumerator.GetNext())
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void GetNext_ThrowsIfFinished()
        {
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            new Action(() => enumerator.GetNext())
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void GetNext_ReturnsNext()
        {
            enumerator.MoveNext();
            enumerator.GetNext().Should().Be(2);
        }

        [Fact]
        public void Dispose_CallsUnderlyingEnumeratorDispose()
        {
            var enumer = new Mock<IEnumerator<int>>();
            var enumerator = new LookupEnumerator<int>(enumer.Object);
            enumerator.Dispose();
            enumer.Verify(x => x.Dispose());
        }

        [Fact]
        public void Reset_CallsUnderlyingEnumeratorReset()
        {
            var enumer = new Mock<IEnumerator<int>>();
            var enumerator = new LookupEnumerator<int>(enumer.Object);
            enumerator.Reset();
            enumer.Verify(x => x.Reset());
        }
    }
}
