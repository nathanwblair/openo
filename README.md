# openo
jQuery 4 C#! Navigate C# Object-Tree like JQuery does the DOM!

## Example usage:
---
### *new* - Create test class
```C#
test_openo testy = new test_openo();
var a = new a();
```
---
### *os*() - Query for first valid Member
* Member **Function** *or* **Variable**
```C#
var start = testy.os("Storte", "Start", "Test");
objecto objecto_j = a.o("j");
```
---

### *o*() - Query for result of Start
```C#
start.o() // Returns void
int j = a.o("j").As<int>(); // j == 100 && a.j == 100;
```
---

