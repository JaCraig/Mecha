# Mecha

The tool needs to do the following things:
1. Fuzz library public entry points.
2. Use DI to automatically create objects for testing.
3. Property test public entry points.
4. Code coverage should be used to determine quality of fuzz/property test values.
5. Decouple from test runner (xUnit.Net, etc.).
6. Allow Fluent interface for creation or just use attributes.
7. Have custom assert library that automatically gets used for test generation/verification.
8. Look into using ML.Net to generate tests/values based on other code.
9. Save failed tests as regression tests.
10. Tests with similar results don't get saved.
11. Use mutation testing.
12. Make suggestions for assertions, etc.
13. Check for nullable objects, etc. and act accordingly.