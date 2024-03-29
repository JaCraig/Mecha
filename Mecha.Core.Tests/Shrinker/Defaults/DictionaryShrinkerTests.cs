﻿using BigBook;
using Mecha.Core.Shrinker.Defaults;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Mecha.Core.Tests.Shrinker.Defaults
{
    /// <summary>
    /// DictionaryShrinker tests
    /// </summary>
    /// <seealso cref="TestBaseClass{Core.Runner.DictionaryShrinker}"/>
    public class DictionaryShrinkerTests : TestBaseClass<DictionaryShrinker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryShrinkerTests"/> class.
        /// </summary>
        public DictionaryShrinkerTests()
        {
            TestObject = new DictionaryShrinker();
        }

        /// <summary>
        /// RunAsync test
        /// </summary>
        [Fact]
        public void CanShrink() => Assert.True(TestObject.CanShrink(new Dictionary<string, string>()));

        /// <summary>
        /// Shrinks this instance.
        /// </summary>
        [Fact]
        public void Shrink()
        {
            var Result = (Dictionary<string, string>)TestObject.Shrink(new Dictionary<string, string> { ["A"] = "B", ["B"] = "A" });
            _ = Assert.Single(Result!);
            Assert.True(Result.TryGetValue("B", out var Value));
            Assert.Equal("A", Value);
        }

        /// <summary>
        /// Shrinks the large dictionary.
        /// </summary>
        /// <param name="length">The length.</param>
        [Property]
        public void ShrinkLargeDictionary([Range(100, 500)] int length)
        {
            var Original = length.Times(x => new Tuple<int, int>(x, 1)).ToDictionary(x => x.Item1, x => x.Item2);
            var Result = (Dictionary<int, int>)TestObject.Shrink(Original);
            Assert.True(Original.Count > Result.Count);
        }
    }
}