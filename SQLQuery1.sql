SELECT SUM(wh) 
                AS totalwh 
                FROM record 
                WHERE id_dev = 1
              AND stamp BETWEEN DATEADD(MINUTE, -50, getdate()) AND DATEADD(MINUTE, -40, getdate())