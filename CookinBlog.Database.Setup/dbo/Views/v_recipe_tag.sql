CREATE VIEW v_recipe_tag
AS

SELECT r2t.RecipeID, r2t.TagID, t.Name AS TagName
FROM recipe_to_tag AS r2t
JOIN tag AS t
ON r2t.TagID = t.ID

