// Copyright © 2014-present Kriasoft, LLC. All rights reserved.
// This source code is licensed under the MIT license found in the
// LICENSE.txt file in the root directory of this source tree.

using Xunit;
using FluentAssertions;

namespace server.test
{
    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dotnet-core.html
    public class SampleTest
    {
        [Fact]
        public void PassingTest()
        {
            2.Should().Be(2);
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}
