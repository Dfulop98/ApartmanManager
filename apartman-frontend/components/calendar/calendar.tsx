'use client';

import { ReactElement, createRef, useCallback, useEffect, useMemo, useRef, useState } from "react";
import Styles from "./calendar.module.css";
import { Day } from "@/types";
import Months from "./months";

interface stateProps{
  startDate : Day | null;
  endDate : Day | null;
  setStartDate: (value: Day | null) => void;
  setEndDate: (value: Day | null) => void;
}
/**
 * Renders a calendar component that allows selecting start and end dates.
 *
 * @return {React.FC} A functional component that renders a calendar.
 */
const CalendarComponent: React.FC<stateProps> = (props): ReactElement => {

  const {startDate, endDate, setStartDate, setEndDate} = props;
  
  const days_header: string[] = [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday",
  ];
  
  const getDaysOfMonth = useCallback((year: number, month: number): Day[] => {
    return Months({params: {year, month}});
  }, []);
  
  const today = new Date();
  const cellRef = useRef<HTMLButtonElement[]>([])
  const [days, setDays] = useState<Day[]>(getDaysOfMonth(today.getFullYear(), today.getMonth()));
  const [selectedMonth, setSelectedMonth] = useState(today.getMonth());
  const [selectedYear, setSelectedYear] = useState(today.getFullYear());
  const memoizedDays = useMemo(() => days, [days]);
  
  cellRef.current = memoizedDays.map((_, i) => cellRef.current[i] ?? createRef());
  
  useEffect(() => {
    const cells = cellRef;
    if(cells !== null){
      
      cells.current.map((button) => {
        
        const dateStr : string | undefined = button.dataset.date;

        if(startDate !== null && endDate !== null){
          if(dateStr !== undefined){
            const currentDate: Date = new Date(dateStr);
           
            (currentDate >= startDate.date && currentDate <= endDate.date)?
              button.classList.add(Styles.selected) :
              button.classList.remove(Styles.selected);
            
          }
        }
        else {
          button.classList.remove(Styles.selected);
        }
      })
    }
    }, [endDate, startDate]);
    
  
  const handleClick = useCallback((day: Day) => {
    
    if(startDate !== null && endDate !== null){

      // start date
      if(startDate === endDate && day.date > endDate.date){
        setEndDate(day);
      }

      else if(day.date === startDate.date){
        setStartDate(null);
        setEndDate(null);
      }
      
      else if (day.date < startDate.date){
        setStartDate(day);
      }
      
      else if (day.date === endDate.date){
        setEndDate(null);
      }
      else if(day.date > endDate.date){
        setEndDate(day);
      }
      else if(day.date > startDate.date && day.date < endDate.date){
        if((endDate.date.getDate() - startDate.date.getDate())/2 > day.date.getDate() - startDate.date.getDate()){
          setStartDate(day);
        }
        else{
          setEndDate(day);
        }
      }
      
    }
    else if(startDate !== null && endDate === null){
      if(day.date > startDate.date){
        setEndDate(day);
      }
      else if(day.date === startDate.date){
        setStartDate(null)
      }
      else{
        setStartDate(day);
      }
    }
    else{
      setStartDate(day);
      setEndDate(day);
      
    }
    
  }, [startDate, endDate, setEndDate, setStartDate]);
  
    
  
  
  const handleMonthChange = useCallback((event: React.ChangeEvent<HTMLSelectElement>) => {
    const [year, month] = event.target.value.split("-");
    setSelectedYear(parseInt(year));
    setSelectedMonth(parseInt(month));
    setDays(getDaysOfMonth(parseInt(year), parseInt(month)));
  }, [getDaysOfMonth]);
  
  return (
    
    <>
      <div className={Styles.month_selection_wrapper}>
        <select className={Styles.month_selection} value={`${selectedYear}-${selectedMonth}`} onChange={handleMonthChange}>
          {Array.from({ length: 12 }, (_, i) => (
            <option key={i} value={`${selectedYear}-${i}`}>
              {new Date(selectedYear, i).toLocaleString("default", { month: "long" })}
            </option>
          ))}
        </select>
      </div>
      <div className={Styles.container}>
        <div className={Styles.container_wrapper}>
          <div className={Styles.calendar_header}>
            {days_header.map((dayName) => (
              <div key={dayName} className={Styles.header_cell}>
                {dayName}
              </div>
            ))}
          </div>
          <div className={Styles.calendar_body}>
            {memoizedDays.map((day, index: number) => (
              <button
                key={`${day.date.getDate()}-${day.date.getMonth()}`}
                ref={(ref : HTMLButtonElement) => (cellRef.current[index] = ref)}
                className={`${Styles.calendar_cell}`}
                onClick={() => handleClick(day)}
                data-date={day.date.toDateString()}
                >
                {day.date.getDate()}
              </button>
            ))}
          </div>
          
        </div>
      </div>
      
    </>
  );
}

export default CalendarComponent;
