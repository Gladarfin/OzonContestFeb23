SELECT 
  p.id, 
  p.contest_id, 
  p.code
FROM problems AS p
JOIN submissions AS s
  on s.problem_id = p.id
WHERE s.success
GROUP BY p.id
HAVING count(distinct s.user_id) > 1
ORDER BY p.id;