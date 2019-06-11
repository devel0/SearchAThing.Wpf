# SearchAThing.Wpf

[![devel0 MyGet Build Status](https://www.myget.org/BuildSource/Badge/devel0?identifier=fc5eb124-c7dc-4d94-b914-6db85ddef2ea)](https://www.myget.org/)

## features

- RTFLog
- SciTextBox

## install and usage

browse [myget instructions](https://www.myget.org/feed/devel0/package/nuget/SearchAThing.Wpf)

## cheatsheet

- math op convert to enable/disable a button depending that a DataGrid selection count great than 1

```cs
IsEnabled="{Binding SelectedItems.Count, ConverterParameter=gte 1, Converter={StaticResource MathOpConverter}, ElementName=dataGrid, Mode=OneWay}"
```

- math op convert to visible/collapse a button depending that a DataGrid selection count great than 1

```cs
Visibility="{Binding SelectedItems.Count, ConverterParameter=gte 1 tovis, Converter={StaticResource MathOpConverter}, ElementName=dataGrid, Mode=OneWay}"
```