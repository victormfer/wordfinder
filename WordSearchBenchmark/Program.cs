﻿// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using WordSearchBenchmark;

var summary = BenchmarkRunner.Run<BenchmarkFinder>();
