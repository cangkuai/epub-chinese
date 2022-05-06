# 1.前言

本人最近看繁体中文的书看的头皮发麻，所以写了一个软件。本软件主要是c#写的，调用了vb的库，最低.net版本为4.5。

# 2.原理解析

epub本质是一个zip文件，那么就能解压后对文字进行处理。经过查找，文字在.\OEBPS\Text文件夹下，标题在.\OEBPS\toc.ncx文件内。那么思路就很清晰了，只要解压后用vb的Microsoft.VisualBasic.Strings.StrConv函数进行逐一转换就好了。

# 3.已知问题

vb的转换库已经非常老了，导致个别字无法转换。我也尝试了第三方库但是转换效率过于慢，感觉没必要。

# 4.运行效果

[![OuAlND.png](https://s1.ax1x.com/2022/05/06/OuAlND.png)](https://imgtu.com/i/OuAlND)
