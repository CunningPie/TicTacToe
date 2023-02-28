import {useRef, useState} from 'react';
import { useNavigate } from 'react-router-dom';

import {Requests} from '../../Requests'

function MatchesPage() {
    const matchIdRef = useRef();
    const lobbyMatchesRef = useRef();
    const [wins, setWins] = useState();
    const [draws, setDraws] = useState();
    const [loses, setLoses] = useState();
    const [rating, setRating] = useState();
    
    const navigate = useNavigate();

    async function JoinMatchHandler(props){
        let matchIdInput = matchIdRef.current.value;

        if (matchIdInput.trim() !== '') {
            if (await Requests.JoinMatchRequest({matchId: matchIdInput}))
            {
                Requests.matchId = matchIdInput;
                Requests.Sym = 'O';
                Requests.yourTurnInit = false;
                navigate('/tictactoe');
            }
        }
    }

    async function CreateMatchHandler(props){
        let matchId = (await Requests.CreateMatchRequest()).replaceAll('"', '');;
        if (matchId !== Requests.emptyGuid)
        {
            Requests.matchId = matchId;
            Requests.Sym = 'X';
            Requests.yourTurnInit = true;;
            navigate('/tictactoe');
        }
    }

    async function RefreshMatchesHandler(props){
        let matches = await Requests.GetLobbyMatchesRequest();
        let stats = await Requests.GetStats();

        let statsVals = stats.replace('[', '').replace(']', '').split(',')

        if (statsVals.length === 4)
        {
            setWins(statsVals[0]);
            setDraws(statsVals[1]);
            setLoses(statsVals[2]);
            setRating(statsVals[3]);
        }

        if (matches.length > 0)
        {
            lobbyMatchesRef.current.value = matches.replace('[', '').replace(']', '').replaceAll('"', '').split(',').join('\n');
        }
    }

    function HistoryHandler(){
        navigate('/history');
    }

    return (
        <div>
            <div>
                <h2>Lobby ({Requests.userGuid})</h2>
                <textarea className='lobby' rows='10' cols='100' ref={lobbyMatchesRef} readOnly={true}></textarea>

                <button className='btn' onClick={RefreshMatchesHandler}>Refresh</button>
            </div>
            <div>
                <div>
                    <label htmlFor='MatchIdInput'>Match Id:</label>
                    <input type="text" id="MatchIdInput" name="MatchId" required ref={matchIdRef}/>

                    <button className="btn" onClick={JoinMatchHandler}>Join</button>
                    <button className="btn" onClick={CreateMatchHandler}>Create</button>
                    <button className="btn" onClick={HistoryHandler}>History</button>
                </div>    
            </div>
            <div>
                <label>Wins: {wins}</label>    
                <label>Draws: {draws}</label>
                <label>Loses: {loses}</label>
                <label>Rating: {rating} </label>
            </div>
        </div>
    )
}

export default MatchesPage;