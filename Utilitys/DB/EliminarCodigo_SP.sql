-- =============================================
-- Author:		<Author,,Samuel Tarazona>
-- Create date: <Create Date,,28/04/2024>
-- Description:	<Description,,Eliminacion del codigo de recuperacion>
-- =============================================
CREATE PROCEDURE EliminarCodigo_SP
	@hora_envio time(0) = NULL
AS
BEGIN
	DECLARE @hora_limite time(0);
	SET @hora_limite = CAST(DATEADD(MINUTE, -5, GETDATE()) AS time(0));
	DELETE FROM dbo.NumRecuperacion
	WHERE @hora_envio < @hora_limite;
END
