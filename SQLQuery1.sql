select * from Field where  fname  like '%�ֱ�%'
--7 21 22 
--10 
--select * from Field where companyid =7 and isdisplay=1 and fname like '%�ֱ�%'
select * from companypayment_7
select * from companycase_7
--��˾���� 7 21 22	�㶫��չ����
select * from CompanyTable where id=7
--ӵ����Ա
select * from UserTable
where id in(select distinct ownerid from  companycase_7)
--update UserTable
--set [password]='MTIz'

--select * from Patch where CompanyID=7 and id=529 order by ExpireDate desc
--and ExpireDate > '2015-01-03 00:00:00'

--update  Patch 
--set [ExpireDate]='2015-12-1'
--where id=529