
import { Day } from '@/types';

interface MonthsProps {
    params:{
        year: number;
        month: number;
    }
}
export default function Months({params}: MonthsProps): Day[] {
    const {year, month} = params; 

    const days: Day[] = [];
    const date = new Date(year, month, 1);
    const firstDayOfWeek = date.getDay();
    const lastDay = new Date(year, month + 1, 0).getDate();

    let day = -firstDayOfWeek;
    while (day < 0) {
      const d = new Date(year, month, 1 + day);
      days.push({
        date: d,
        dayOfWeek: d.getDay(),
        isCurrentMonth: false,
        isSelected: false,
      });
      day++;
    }

    for (let i = 1; i <= lastDay; i++) {
      const d = new Date(year, month, i);
      days.push({
        date: d,
        dayOfWeek: d.getDay(),
        isCurrentMonth: true,
        isSelected: false,
      });
    }

    let nextMonthDay = 1;
    while (days.length % 7 !== 0) {
      const d = new Date(year, month + 1, nextMonthDay);
      days.push({
        date: d,
        dayOfWeek: d.getDay(),
        isCurrentMonth: false,
        isSelected: false,
      });
      nextMonthDay++;
    }

    return days;
}