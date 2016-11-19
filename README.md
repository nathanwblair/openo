# OpenO

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
var testy_start = testy.os("Storte", "Start", "Test"); // Query function
objecto objecto_j = a.o("j"); // Query variable
```
---

### *o*() - Query for result of Start
```C#
testy_start.o() // Returns void
// NOW: testy.isTrue == false;

int j = a.o("j").As<int>(); // returns 100 (value of a.j)
//NOW: j == 100 && a.j == 100;
```
---

