import {useState} from 'react';

import SignInModal from './SignInModal';
import Backdrop from './Backdrop';

function SignInButton() {
    const [signInModalIsOpen, setSignInModalIsOpen] = useState(false);

    function openSignInModalHandler() {
        setSignInModalIsOpen(true);
    }

    function closeSignInModalHandler(){
        setSignInModalIsOpen(false)
    }

    return (
        <div>
            <div>
                <button className="btn" onClick={openSignInModalHandler}>
                    Sign In
                </button>
            </div>
        {signInModalIsOpen && <SignInModal onClose={closeSignInModalHandler}/>}
        {signInModalIsOpen && <Backdrop onCancel={closeSignInModalHandler}/>}
        </div>
    );
}
  
export default SignInButton;