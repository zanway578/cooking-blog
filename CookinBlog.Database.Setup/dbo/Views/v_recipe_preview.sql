
CREATE VIEW v_recipe_preview
AS
SELECT r.Id AS RecipeID,
	r.Name,
	r.Description,
	rm.FileName,
	r.IsPublished
FROM recipe AS r
LEFT JOIN recipe_to_media AS rm
ON r.ID = rm.RecipeID
WHERE COALESCE(rm.MediaCategory, 0) = 0 AND COALESCE(rm.MediaNumber, 1) = 1;

