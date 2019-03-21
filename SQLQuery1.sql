

SELECT stamp, voltage, current_l1 + current_l2 + current_l3 AS totalCurrent, wh
FROM record 
WHERE id_dev = 1 
AND stamp 
BETWEEN DATEADD (hh, -1, getdate()) AND GETDATE()