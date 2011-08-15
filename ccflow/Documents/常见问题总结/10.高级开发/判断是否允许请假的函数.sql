set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

ALTER FUNCTION [dbo].[CheckIsCandiaoxiu]
(
	@Rec VARCHAR(128),
@Days int
) RETURNS varchar(300) AS
BEGIN

  DECLARE @result varchar(300)
  DECLARE @JiabanDays INT
  DECLARE @diaoxiuDays INT
     
  --SELECT @JiabanDays=SUM(jiabantianshu) FROM ND301  WHERE  WFState=1 and Rec=@Rec 
 -- SELECT @diaoxiuDays= SUM(tianshu) FROM ND1801 WHERE  Nodestate=0 and Rec=@Rec 

SET @JiabanDays = (SELECT SUM(jiabantianshu) FROM ND301 WHERE  WFState=1 and Rec=@Rec )
  SET @diaoxiuDays =( SELECT SUM(tianshu) FROM ND1801 WHERE  WFState=1 and Rec=@Rec)+@Days

	IF @JiabanDays >= @diaoxiuDays
		BEGIN
		   SET @result = '1'
		END
	ELSE
		BEGIN
		  -- SET @result = '1'

  SET @result =  '调休天数不符合调休条件!!!'
		END
	RETURN @result 
END

/*
SELECT dbo.CheckIsCandiaoxiu('yzl',0)

 SELECT SUM(jiabantianshu) FROM ND301 WHERE  WFState=1 and Rec='yzl'
SELECT SUM(tianshu) FROM ND1801 WHERE  WFState=1 and Rec='yzl'

SELECT * FROM ND1801

UPDATE ND1801 SET tianshu=0
*/