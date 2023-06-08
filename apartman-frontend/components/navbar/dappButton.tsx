"use client"
import { useEffect, useState } from "react";
import styles from './Navbar.module.css'
import { ethers } from 'ethers'
import Image from 'next/image'
import Globeicon from '../../public/Globeicon.svg'


export default function DappButton() {
    
  const [isConnected, setIsConnected] = useState(false);
  const [hasMetamask, setHasMetamask] = useState(false);
  const [signer, setSigner] = useState(undefined);

  useEffect(() => {
    if (typeof window.ethereum !== "undefined") {
      setHasMetamask(true);
    }
  });
  async function connectDapp() {
    
    if(hasMetamask){
      try{
        await ethereum.request({
          method: 'eth_requestAccounts',
        })
        setIsConnected(true);
        let connectedProvider = new ethers.BrowserProvider(window.ethereum);
        setSigner(connectedProvider.getSigner());
      } catch(e){
        console.log(e)
      }
    }else{
      setIsConnected(false);
    }
  }
  
  return (
  <>
    {isConnected ? (
    <button className={styles.dapp}>
        <Image src={Globeicon} alt="globe-icon" width={26} height={26}
          className={styles.globe_icon}/>
          Connected
        </button>
        ) : (
      <button onClick={() => connectDapp()} className={styles.dapp}>
        <Image src={Globeicon} alt="globe-icon" width={26} height={26}
          className={styles.globe_icon}/>
          Connect Wallet
        </button>

    ) }
  </>
  )
}
