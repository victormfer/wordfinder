```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3880/23H2/2023Update/SunValley3)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.200
  [Host]     : .NET 6.0.27 (6.0.2724.6912), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.27 (6.0.2724.6912), X64 RyuJIT AVX2


```
| Method         | Mean     | Error   | StdDev  | Gen0    | Allocated |
|--------------- |---------:|--------:|--------:|--------:|----------:|
| BenchFindLarge | 100.8 μs | 1.17 μs | 0.97 μs | 23.1934 |  94.77 KB |
