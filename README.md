# xbr算法平滑像素画

Unity用像素画转换为非像素画的xbr算法。

算法cg源代码取自[libretro/common-shaders](https://github.com/libretro/common-shaders/blob/master/xbrz/shaders/4xbrz.cg)。

效果如下：

![](https://github.com/llapuras/XbrShader/blob/master/display.png)


# 使用tips

- 图片scale、像素单位的长度以及perspective模式下的camera距离都会影响效果（猜测这三个因素都影响了shader处理的分辨率）
- 建议把camera调成ortho模式，画面效果就不受camera距离影响了，这样也可以排列图片在z轴的先后顺序
- 在ortho模式下，对于同一像素单位长度的图片，其图片scale、像素单位长度以及v_resolution的乘积总是等于固定值（原因不明）。同一品屏幕内的像素画需要按照统一像素单位长度进行绘制。
- 这个算法适用于纯粹的像素sprite，用于生成某种圆润的非像素画风。

根据目前的试验得出以下数据（ortho模式下）：
|  单位像素长度   | v_resolution值  | scale |
|  ----  | ----  | ---- |
| 1  | 334 | 3 |
| 3  | 334 | 1 |
| 2  | 498 | 1 |
| 4  | 249 | 1 |
| 7  | 148 | 1 |
- 如果需要调整scale，例如scale x 2，那么v_resolution则需要除以2，保证乘积不变
- 如果单位像素长度为1，通常需要放大scale，否则根据xbr将一个像素划分为5x5进行处理的做法，最后得到的只会是一团糊糊
