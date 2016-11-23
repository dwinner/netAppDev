<arithmetic () Python>
def myadd(x, y): return x + y
<return(myadd)>

<addition () include Scripts\\add3.py>
<return(add3)>

<calculation (myadd, add3) Ruby>
puts myadd.call(5, 2)
puts add3.call(2)
<return()>

