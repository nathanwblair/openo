# openo
jQuery 4 C#! Navigate C# Object-Tree like JQuery does the DOM!

## Example usage:
---
### *new* - Create test classes
```C#
test_openo testy = new test_openo();
// testy.isTrue == true && testy.isFalse = false;

var a = new a();
// a.i == 0 && a.j == 100;
```
---
### *os*() - Query for first valid Member
* Member **Function** *or* **Variable**
```C#
var start = testy.os("Storte", "Start", "Test"); // Query function
objecto objecto_j = a.o("j"); // Query variable
```
---

### *o*() - Query for result of Start
```C#
start.o() // Returns void
// testy.isTrue == false;

int j = a.o("j").As<int>(); // returns 100 (value of a.j)
// j == 100 && a.j == 100;
```
---

