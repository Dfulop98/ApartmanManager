'use client';
import { useState } from "react";
import styles from './Navbar.module.css'
import {ethers} from 'ethers'
export default function DappButton() {
    
  const [isConnected, setIsConnect] = useState(false);
  const [provider, setProvider] = useState();
  async function connectDapp() {
    
    if(typeof window.ethereum !== "undefined"){
      try{
        await ethereum.request({
          method: 'eth_requestAccounts',
        })
        setIsConnect(true);
        let connectedProvider = new ethers.BrowserProvider(window.ethereum);
        setSigner(connectedProvider.getSigner());
      } catch(e){
        console.log(e)
      }
    }else{
      setIsConnect(false);
    }
  }
  return (
    <button onClick={connectDapp} className={styles.dapp}>Dapp</button>
  )
}
