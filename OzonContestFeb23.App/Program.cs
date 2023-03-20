using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using OzonContestFeb23App.Models;
using OzonContestFeb23App.Solutions;

//менять на то, которое хотим тестировать ContestFeb23B, ContestFeb23C, etc.
var cont = new ContestFeb23I();

//путь до файла JSON для соответвующего задания
var path = @"";

var data = File.ReadAllText(path);

//Модель соответствует выбранному заданию из контеста
var json = JsonConvert.DeserializeObject<List<ModelI>>(data);
var timeMemory = new List<TimeAndMemoryModel>();

var i = 1;
//замеры памяти и времени для каждого теста
foreach (var model in json)
{
        var sw = Stopwatch.StartNew();
        var memBefore = GC.GetAllocatedBytesForCurrentThread();
        cont.Solve(model.TableInHtml);
        var memAfter = GC.GetAllocatedBytesForCurrentThread();
        var memAllocTotal = memAfter - memBefore;
        sw.Stop();
        timeMemory.Add(new TimeAndMemoryModel {
            TestNumber = i, 
            ElapsedTime = sw.Elapsed.TotalSeconds, 
            MemoryAllocatedBytes = memAllocTotal });
        Console.WriteLine($"Current test: {i}");
        i++;
}

var output = JsonConvert.SerializeObject(timeMemory, Formatting.Indented);
//путь куда сохранять результаты замеров для тестов
var saveTo = @"";
File.WriteAllText(saveTo, output);
