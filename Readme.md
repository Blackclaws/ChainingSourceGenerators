Minimal Repro to show that two source generators can indeed chain on the PostInitialization output of each other (not to be confused with the output of Execute)

To run just do:
```
dotnet run --project TestApp
```

What is the basic idea:

```GeneratorOne``` is introducing an attribute ```Test``` where classes that have this attribute get a companion partial that can ``SayHello()``

```GeneratorTwo``` produces a class as part of its PostInitialization that uses the attribute and subsequently gets its ``SayHello`` method.