SELECT
  RANK() OVER (ORDER BY COUNT(problem_id) DESC, MAX(submitted_at) ASC),
  user_id,
  user_name,
  COUNT(problem_id) AS problem_count,
  MAX(submitted_at) AS latest_successful_submitted_at
FROM (
  SELECT
    user_id,
    users.name AS user_name,
    MIN(CASE WHEN sm.success THEN sm.submitted_at END) AS submitted_at,
    CASE WHEN sm.success THEN problem_id END AS problem_id
  FROM submissions AS sm
  JOIN problems AS p ON(p.id=sm.problem_id)
  JOIN users ON(users.id=sm.user_id)
  WHERE p.contest_id = (
    SELECT MAX(id)
    FROM contests
  )
  GROUP BY user_id, user_name, problem_id, sm.success
) AS T1
GROUP BY user_id, user_name
ORDER BY problem_count DESC, latest_successful_submitted_at, user_id;