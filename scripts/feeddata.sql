delete CustomFieldProviderValue
delete [dbo].[CustomFields]
delete ProviderService
delete Providers
delete Services

declare @i int
set @i=0
while (@i<=24)
begin
	set @i=@i+1
	INSERT INTO [dbo].[Providers]
           ([Name]
           ,[InternalCode]
           ,[Email]
           ,[Phone])
     VALUES
           ('provider'+convert(nvarchar(2),@i),
           'nit'+convert(nvarchar(2),@i),
		   'email'+convert(nvarchar(2),@i),
		   'phone'+convert(nvarchar(2),@i)
		   )
end



set @i=500
while (@i<=550)
begin
	set @i=@i+1
	INSERT INTO [dbo].[Services]
           ([Name]
           ,[HourlyPrice])
     VALUES
           ('service'+convert(nvarchar(3),@i),
		   100.30+@i
		   )
end


declare @muestraProvider table(idProvider int)
insert @muestraProvider
select top 10 IdProvider from Providers 


declare @muestraServices table(idService int)
insert @muestraServices
select top 10 IdService from Services 



insert [dbo].[ProviderService]
select IdProvider,IdService,'col'
from @muestraProvider inner join @muestraServices on 1=1


set @i=1
while (@i<=10)
begin
	set @i=@i+1
	INSERT INTO [dbo].CustomFields
           ([IdCustomField]
           ,[Name]
		   ,[FieldType])
     VALUES
           ('CF'+convert(nvarchar(3),@i),
		   'custom field '+convert(nvarchar(3),@i),
		   'text'
		   )
end

declare @muestracustomfields table([IdCustomField] nvarchar(20))

insert @muestracustomfields
select [IdCustomField] from CustomFields


insert CustomFieldProviderValue
select [IdCustomField],IdProvider,'valuexx'
from @muestracustomfields inner join  @muestraProvider on 1=1





