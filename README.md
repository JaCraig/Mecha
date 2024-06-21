![Mecha Logo](https://repository-images.githubusercontent.com/337571372/3bfdaf00-a467-11eb-9e15-c30893f31981)

 [![.NET Publish](https://github.com/JaCraig/Mecha/actions/workflows/dotnet-publish.yml/badge.svg)](https://github.com/JaCraig/Mecha/actions/workflows/dotnet-publish.yml) [![Coverage Status](https://coveralls.io/repos/github/JaCraig/Mecha/badge.svg?branch=main)](https://coveralls.io/github/JaCraig/Mecha?branch=main)

> I try to make idiot proof software but the world keeps making better idiots.

Or at least that's how it seems to us much of the time. You post a new application feature and the user's find a way to break it. In reality it's because we didn't think of an edge case that they found. I mean who can possibly think up all of these possibilities? A cracked out squirrel? Perhaps a raccoon with ADHD? Neither really work out in the long run as eventually they rise up against society to overthrow it. They're trained with computers at that point so there is no stopping them. And personally I'm too lazy to do this myself so I guess that leaves writing some code to do it, but thankfully I've already done that for you.

Mecha is a library to automatically test classes with the ultimate goal being to find ways to break the code. It can help with unit testing, security testing via data fuzzing, testing for concurrency issues, and check for fault tolerance. With one line of code, you can test every method in a class automatically. Best part is it ties into the testing framework that you're already using.

# The Code and What It's Doing

    Mech.BreakAsync<MyClass>();

Seriously, that's it. With that Mecha searches for any public methods and properties, loops through them, finds interesting inputs for the methods, and runs them looking for exceptions to be thrown. Once an exception is thrown, it then uses the input to find the smallest possible value that still results in an issue. It then saves these issues as test cases for the future.

Ok, so it breaks my method and does what? Mecha uses a concept borrowed from property testing known as shrinking. For instance you generate a list of 100 items for the method and it breaks. Mecha then removes some of the values and sees if it continues to break. It then continues to do this until it finds a list as small as possible that still breaks.

# Supported Types and Object Generation

Mostly everything. It will generate inputs for all value types, enums, and even complete classes. It will give your methods FileStreams, HttpClients, Streams, etc. that throw exceptions themselves to see if your code can handle edge cases. Stubs/substitutes are automatically generated and fed through depending on the parameters of your method. It checks bounds, null, etc. as well. On top of that it takes into account any ValidationAttributes that it finds. So if you specify a range for a parameter, it will keep the values inside that range.

# Non Ninja Mutants

On a successful run where no exception is thrown the input values are potentially mutated in ways that are known to cause issues. For instance adding \0 in the middle of a string, etc. These are then run through the system to see if they can break the code. If they do, they're added to the set of results. If not, we mutate again and rerun up until the max number of mutations that you set in the options.

# Shrinking All the Things

On a run where an exception is thrown, the input values are "shrunk". This involves taking a value and making it smaller in some way. For instance if a list of 100 values throws an exception, the system will remove 20 of them and try again. If it throws an exception, we shrink it again and again until it passes or we hit the max number of shrinks set in the options.

# Getting Started

All you need to do is install the framework specific library from NuGet:

| Framework | Library |
|-----------|---------|
| xUnit.Net | [Mecha.xUnit](https://www.nuget.org/packages/Mecha.xUnit/) |
| All other frameworks | [Mecha.Core](https://www.nuget.org/packages/Mecha.Core/) |

This list should grow as new supported frameworks are added. If your library isn't supported, you can get access to the Mech class itself by adding the Mecha.Core library to your project. And if you like the library and would like to support your framework of choice, we'll gladly accept pull requests to add it. Anyway, what does the framework specific libraries give you?

## xUnit.Net

    [Property]
    public void MyTest(int value1,int value2, ...)
    {
        ...
    }

The above code adds the Property attribute which lets xUnit.Net know that you want to assign values for value1, value2, etc. using the library. It then aggregates all of the failed tests in one result in the Test Explorer window with details about what values broke the test.

## Everything Else

    Mech.BreakAsync<MyClass>();

There are variations of the above method depending on your needs. Also note that you can specify various options including how long it should attempt to generate values, the number of shrinks to attempt, and the number of test cases to run from the generated values.

# FAQ

1. What if I have a method that I don't want the library to run?

    A. The library comes with a DoNotBreak Attribute. Any method marked with this will be skipped by the library.

2. Where is it saving the failed runs for later?

    A. By default the system places the failed runs in a directory called Mecha under the test library's bin directory. It's possible to override this and store the information somewhere else by implementing the Mecha.Core.Datasources.Interfaces.IDatasource interface.

3. Can I create my own data generators?

    A. Yes by implementing the Mecha.Core.Generator.Interfaces.IGenerator interface. The system should automatically pick them up and use them.

4. Can I create my own data shrinker?

    A. Yes by implementing the Mecha.Core.Shrinker.Interfaces.IShrinker interface. The system should automatically pick them up and use them.

5. Can I create my own data mutator?

    A. Yes by implementing the Mecha.Core.Mutator.Interfaces.IMutator interface. The system should automatically pick them up and use them.
