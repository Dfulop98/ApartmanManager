'use client'

import { ReactElement, useRef, useState } from "react";
import {useRouter} from 'next/navigation'
import {useForm, SubmitHandler, Resolver} from "react-hook-form";
import _ from "lodash"
import cn from "classnames"
import CalendarComponent from "../calendar/calendar";
import CountrySelection from "./countrySelection";

import Styles from "./reservationForm.module.css";

import { Day, FormValues } from "@/types";


interface roomProps {
  roomId: string,
  roomCapacity: number
}

const ReservationForm: React.FC<roomProps> = (props): ReactElement => {

  const router = useRouter();
  const [startDate, setStartDate] = useState<Day | null>(null);
  const [endDate, setEndDate] = useState<Day | null>(null); 
  const countryRef = useRef<HTMLSelectElement>(null);
  const {roomId, roomCapacity} = props;

  const resolver: Resolver<FormValues> =async (values) => {
    return {
      values: values.firstName ? values: {},
      errors: !values.firstName ? {
        firstName: {
          type: "required",
          message: "First Name is required"
        },
      }:{},
    }    
  }

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormValues>({
    defaultValues:{
      firstName: ""
    },
    resolver
  })  
  
  const onSubmit: SubmitHandler<FormValues> = (data) => 
  {
    router.push('/reservation-confirm')
  }

    return (
      <>
      <CalendarComponent startDate={startDate} endDate={endDate} setStartDate={setStartDate} setEndDate={setEndDate} />
        <form onSubmit={handleSubmit(onSubmit)}>

          <div className={Styles.container}>
            <p className={Styles.header}> Personal datas</p>
            <div className={Styles.line_wrapper}>
              <div className={Styles.form}>
                <input placeholder=" "
                className={`${Styles.input} `}
                {...register("firstName")}/>
                {errors?.firstName && <p>{errors.firstName.message}</p>}
                <label className={Styles.label}>First Name</label>
              </div>

              <div className={Styles.form}>
                <input placeholder=" " {...register("lastName")} className={Styles.input}/>
                <label className={Styles.label}>Last Name</label>
              </div>
            </div>

            <div className={Styles.line_wrapper}>
              <div className={Styles.form}>
                <input placeholder=" " {...register("nationality")} className={Styles.input}/>
                <label className={Styles.label}>Nationality</label>
              </div>
              <div className={Styles.form}>
                <input placeholder=" " {...register("postalCode")} className={Styles.input}/>
                <label className={Styles.label}>Postal code</label>
              </div>
            </div>

            <div className={Styles.line_wrapper}>
              <div className={Styles.form}>
                <CountrySelection countryRef={countryRef}/>
              </div>
              <div className={Styles.form}>
                <input placeholder=" " {...register("province")} className={Styles.input}/>
                <label className={Styles.label}>Province</label>
              </div>
              <div className={Styles.form}>
                <input placeholder=" " {...register("city")} className={Styles.input}/>
                <label className={Styles.label}>City</label>
              </div>
            </div>
            <p className={Styles.header}> Reservation details</p>
            <div className={Styles.line_wrapper}>
              <div className={Styles.form}>
                <label className={Styles.title}>People</label>
                <select className={Styles.select} {...register("numberOfGuest")}>
                  {_.times(roomCapacity, (i) => (
                    <option value={i+1}>{i+1}</option>
                  ))}
                </select>
              </div>
              <div className={cn(Styles.form, Styles.pets_form)}>
                <label className={Styles.title}>Any pets</label>
                <input className={Styles.checkbox} type="checkbox" {...register("pets")}/>
              </div>
              <div className={cn(Styles.form, Styles.textarea_form)}>
                <label className={Styles.title}> Message to the owner </label>
                <textarea {...register("description")} className={Styles.textarea}></textarea>
              </div>
            </div>
            <div className={Styles.form}>
              <input type="submit" className={Styles.input} value="Confirm"/>
                  
            </div>
          </div>
        </form>
      </>
    )
}

export default ReservationForm;