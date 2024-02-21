WITH RECURSIVE chiefTree (id, chief_id, depth) AS (
	SELECT id, chief_id, 0 AS depth
    FROM employee
    WHERE chief_id IS NULL
    
    UNION 
    
    SELECT employee.id, employee.chief_id, depth + 1
    FROM chiefTree temp
    JOIN employee ON employee.chief_id = temp.id    
)
SELECT MAX(depth)
FROM chiefTree