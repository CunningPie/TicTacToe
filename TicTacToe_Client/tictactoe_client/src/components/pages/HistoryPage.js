import {useRef} from 'react';
import { useNavigate } from 'react-router-dom';

import {Requests} from '../../Requests'

function HistoryPage() {
    const historyMatchIdRef = useRef();
    const finishedMatchesRef = useRef();
    const matchHistoryRef = useRef();
    const navigate = useNavigate();

    async function OpenMatchHistoryHandler(props){
        let historyMatchIdInput = historyMatchIdRef.current.value;

        let matchHistory =  await Requests.GetMatchHistoryRequest({matchId: historyMatchIdInput});
        alert(historyMatchIdInput);

        matchHistoryRef.current.value = matchHistory.replaceAll('"', '').replace('[', '').replace(']', '').split(',').join('\n');
    }

    async function RefreshMatchesHandler(props){
        let matches = await Requests.GetFinishedMatchesRequest();

        if (matches)
        {
            finishedMatchesRef.current.value = matches.replace('[', '').replace(']', '').replaceAll('"', '').split(',').join('\n');
        }
    }

    function BackHandler()
    {
        navigate('/matches');
    }

    return (
        <div>
            <div>
                <h2>Matches</h2>
                <textarea className='lobby' rows='10' cols='100' ref={finishedMatchesRef} readOnly={true}></textarea>

                <button className='btn' onClick={RefreshMatchesHandler}>Refresh</button>
            </div>
            <div>
                <div>
                    <label htmlFor='historyMatchIdInput'>Match Id:</label>
                    <input type="text" id="historyMatchIdInput" name="HistoryMatchId" ref={historyMatchIdRef}/>

                    <button className="btn" onClick={OpenMatchHistoryHandler}>Open</button>
                </div>    
            </div>
            <div>
            <h2>Match</h2>
                <textarea className='lobby' rows='10' cols='100' ref={matchHistoryRef} readOnly={true}></textarea>
            </div>

            <button className="btn" onClick={BackHandler}>Back</button>
        </div>
    )
}

export default HistoryPage;