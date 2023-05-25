import axios from 'axios';
import https from 'https';

export default async function getAllRoom() {
    const agent = new https.Agent({  
      rejectUnauthorized: false
    });

    const res = await axios.get('https://localhost:7223/api/room', { httpsAgent: agent });

    if(res.status !== 200) throw new Error('Failed to fetch data');

    return res.data;
}
