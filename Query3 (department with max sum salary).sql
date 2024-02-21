SELECT department_id, SUM(salary) AS salary_sum
FROM employee
GROUP BY department_id
ORDER BY salary_sum DESC
LIMIT 1