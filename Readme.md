# fbregistrar - FlitBit.Registrar

A small framework for maintaining registrations in .NET.

## What

This library contains a set of interface declarations for maintaining a registry of things. It also provides a thread-safe, non-persistent implementation.

## Why

Keeping a registry of things is a [common pattern](http://martinfowler.com/eaaCatalog/registry.html) used to map keys to objects. To be sure, like many other patterns [the Registry Pattern can be misused and abused](http://www.brandonsavage.net/the-registry-pattern-reexamined/), but like `Dictionary<K,V>` - it is valuable when scoped appropriately.

We wanted a simple, reusable implementation that would provide some consistency in our higher-order frameworks that need to track registered things.

## How

```
// TODO: some useful documentation
```