SELECT
  u.id,
  u.name,
  COUNT(DISTINCT(
    CASE
      WHEN sm.success THEN c.id
    END
  )) AS solved_at_least_one_contest_count,
  COUNT(DISTINCT c.id) AS take_part_contest_count
FROM users AS u
LEFT JOIN submissions AS sm ON(sm.user_id=u.id)
LEFT JOIN problems AS p ON(sm.problem_id=p.id)
LEFT JOIN contests AS c ON(c.id=p.contest_id)
GROUP BY u.id
ORDER BY
  solved_at_least_one_contest_count DESC,
  take_part_contest_count DESC,
  u.id;