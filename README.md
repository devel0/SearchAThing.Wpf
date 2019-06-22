# SearchAThing.Wpf

[![NuGet Badge](https://buildstats.info/nuget/netcore-util)](https://www.nuget.org/packages/SearchAThing.Wpf/)

## features

- RTFLog
- SciTextBox
- StatusManager

## install

- [nuget package](https://www.nuget.org/packages/SearchAThing.Wpf/)

## cheatsheet

- math op convert to enable/disable a button depending that a DataGrid selection count great than 1

```cs
IsEnabled="{Binding SelectedItems.Count, ConverterParameter=gte 1, Converter={StaticResource MathOpConverter}, ElementName=dataGrid, Mode=OneWay}"
```

- math op convert to visible/collapse a button depending that a DataGrid selection count great than 1

```cs
Visibility="{Binding SelectedItems.Count, ConverterParameter=gte 1 tovis, Converter={StaticResource MathOpConverter}, ElementName=dataGrid, Mode=OneWay}"
```
