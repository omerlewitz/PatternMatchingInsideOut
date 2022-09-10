using static System.Console;

#region Constnat Pattern

static void ConstantPattern()
{
    // This is boxing because in the right hand side its value type and
    // Object is a reference type. Meaning, The value 42 moved to the heap
    // and we have a reference to the box
    object something = 42;

    // The following statement would not work( you cannot use == with object and int).
    // We need to use `Equals` instead.
    // if (something == 42) ...
    if (something.Equals(42)) Console.WriteLine("Something is 42");

    // Now we can write this much nicer:
    //              +---- Constant pattern
    //             V
    if (something is 42) Console.WriteLine("Something is 42");
}

ConstantPattern();

#endregion

#region Type Pattern

// In the past we would have written...
static void GoodOldTypeCheck()
{
    // Note data type `object` in the following line
    object o = new Hero("Wade", "Wilson", "Deadpool", HeroType.FailedExperiment, false);
    var h = o as Hero;
    if (h != null) WriteLine($"o is a Hero and is called {h.HeroName}");
}

GoodOldTypeCheck();

// Now we can be much more concise using "Type Pattern"
static void NewTypePattern()
{
    // Note data type `object` in the following line
    object o = new Hero("Wade", "Wilson", "Deadpool", HeroType.FailedExperiment, false);

    //    +--- Type pattern
    //    |       +--- Definite assignment
    //    V       V
    if (o is Hero hero) WriteLine($"o is a Hero and is called {hero.HeroName}");

    // var hero = 42; // does not work because h is already defined
    // WriteLine(hero.HeroName); // does not work because hero is unassigned outside of `if` block.

    // Avoid the following line because it is confusing
    if (!(o is Hero h2)) WriteLine("No hero");
    else WriteLine($"We have a hero named {h2.HeroName}"); // <-- because of `not` h2 is defined in `else`

    // Prefer `is not` if you want to negate condition
    if (o is not Hero h3) WriteLine("No hero");
}

NewTypePattern();

// Also works nice with collections
static void TypePatternAndCollections()
{
    // Note that Person is the base class of Hero
    IEnumerable<Person> pEnumerable = new Person[]
    {
        new("John", "Doe"),
        new Hero("Wade", "Wilson", "Deadpool", HeroType.FailedExperiment, false)
    };

    //  +-- Two Type Patterns + definite assignments -+
    //  V                                             V
    if (pEnumerable is IReadOnlyList<Person> pList && pList[1] is Hero h)
    {
        WriteLine($"o is a Hero and is called {h.HeroName}");
    }
}
TypePatternAndCollections();

static void NullCheckWithTypePattern()
{
    Hero? someone = null;
    //  +-- Type Pattern     +-- h can be used in subsequent parts of expression
    //  V                    V
    if (someone is Hero h && h.Type == HeroType.FailedExperiment)
    {
        WriteLine($"Someone is the {h.HeroName} hero and not null");
    }
    else WriteLine("Someone is null");

    //  +-- Constant pattern with null
    //  V
    if (someone is null) WriteLine("Someone is null");
    
}
NullCheckWithTypePattern();
#endregion

#region Type pattern in Switch
WriteHeaderLine("Type Patterns in `Switch` statement");
// Note data type `object` in the following line
object o = new Hero("Wade", "Wilson", "Deadpool", HeroType.FailedExperiment, false);


#endregion

#region Helper methods and data structures

static void WriteHeaderLine(string message)
{
    var oldFo = ForegroundColor;
    ForegroundColor = ConsoleColor.Yellow;
    WriteLine($"\n{message}");
    ForegroundColor = oldFo;
}

enum HeroType
{
    NuclearAccident,
    FailedExperiment,
    Alien,
    Mutant,
    Technology,
    Other
};

enum HeroTypeCategory
{
    Accident,
    SuperPowersFromBirth,
    Other
}

enum VoughtEmployeeType
{
    TopManagement,
    TheSeven,
    LocalHero,
    RegularPerson
};

record Person(string FirstName, string LastName, int? Age = null, Person? Assistant = null);

record Hero(string FirstName, string LastName, string HeroName, HeroType Type,
    bool CanFly, Person? Assistant = null) : Person(FirstName, LastName, Assistant: Assistant);

record JumpingHero(string Name, int MaxJumpDistance);

#endregion