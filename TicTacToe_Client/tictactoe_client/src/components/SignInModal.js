import {useRef} from 'react';
import { useNavigate } from 'react-router-dom';

import {Requests} from '../Requests'

function SignInModal(props) {
    const usernameInputRef = useRef();
    const passwordInputRef = useRef();
    const navigate = useNavigate();

    async function signInHandler() {
        let usernameInput = usernameInputRef.current.value;
        let passwordInput = passwordInputRef.current.value;

        if (usernameInput.trim() !== "" && passwordInput.trim() !== "")
        {
            let requestResult = await Requests.SignInRequest({username : usernameInput, password : passwordInput});
            requestResult = requestResult.replaceAll('"', '');

            if (requestResult !== "00000000-0000-0000-0000-000000000000")
            {
                Requests.userGuid = requestResult;
                props.onClose();
                navigate('/matches');
            }
            else
            {
                alert("Invalid username or password");
            }
        }
    }

    return (
        <div className='modal'>
            <div className='input'>
            <label htmlFor='UsernameInput'>Username:</label>
            <input type="text" id="UsernameInput" name="Username" required
                minLength="4" maxLength="8" size="10" ref={usernameInputRef}/>
            </div>    

            <div className="input">
            <label htmlFor='PasswordInput'>Password:</label>
            <input type="password" id="PasswordInput" name="Password" required
                minLength="4" maxLength="8" size="10" ref={passwordInputRef}/>
            </div>

            <button className="btn" onClick={signInHandler}>Sign In</button>
        </div>
    );
}

export default SignInModal;