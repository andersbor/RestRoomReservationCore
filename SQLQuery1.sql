select * from roomreservationReservation where cast('2018-10-02' as date) 
between cast(fromTime as date) and cast(toTime as date);


SELECT CONVERT(date, getdate());

select * from roomreservationReservation order by fromtime;

SELECT roomreservationReservation.*, DATEDIFF(day, thedate, getdate()) AS dd 
from roomreservationreservation
where abs(DATEDIFF(day, thedate, '2018-10-01')) < 1.1;

select *, convert(date,'2018-10-02') from roomreservationReservation where roomid=1 and thedate =  convert(date,'2018-10-02');

select * from roomreservationReservation 
where roomid=1 and convert(date, '01-10-2018') between cast(fromTime as date) and cast(toTime as date) order by fromTime