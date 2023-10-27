CREATE PROCEDURE [dbo].[GetArtistDetails]
	@artistID int = 0
AS
BEGIN

	SELECT [title], [biography], [imageURL], [heroURL]
	FROM Artist 
	WHERE artistID = @artistID

	SELECT [albumID], [title], [year], [imageURL]
	FROM Album
	WHERE artistID = @artistID
	
	SELECT [songID], s.title as [title], [bpm], [imageURL], a.title as [album]
	FROM Song s
	INNER JOIN Album a ON a.albumID = s.albumID
	WHERE s.artistID = @artistID

END