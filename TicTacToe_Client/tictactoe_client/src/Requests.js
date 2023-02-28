class Requests {
    static Sym;
    static auth;
    static userGuid;
    static matchId;
    static yourTurnInit;
    static matchStatus = -1;
    static get emptyGuid() {return "00000000-0000-0000-0000-000000000000"; }

    static async SignUpRequest(props) {
        return fetch('https://localhost:7241/Auth/SignUp?' + new URLSearchParams({
            username: props.username, password: props.password}), {method: 'POST'})
        .then(response => {
            if (response.status === 200)
            {
                let credentials = btoa(props.username + ":" + props.password);
                this.auth = { "Authorization" : `Basic ${credentials}` };
                return response.text();
            }
            
            return this.emptyGuid;
        });
    }

    static async SignInRequest(props) {
        return fetch('https://localhost:7241/Auth/SignIn?'+ new URLSearchParams({
            username: props.username, password: props.password}), {method: 'POST'})
        .then(response => {
            if (response.status === 200)
            {
                let credentials = btoa(props.username + ":" + props.password);
                this.auth = { "Authorization" : `Basic ${credentials}` };
                return response.text();
            }
            return this.emptyGuid;
        });
    }

    static async GetUsersRequest() {
        return fetch('https://localhost:7241/Auth', { headers : this.auth })
        .then(response => {
            if (response.status === 200)
            {
                return response.text();
            }

            return ['wrong'];
        });
    }

    static async GetLobbyMatchesRequest() {
        return fetch('https://localhost:7241/Game/LobbyMatches', { headers : this.auth })
        .then(response => {
            if (response.status === 200)
            {
                return response.text();
            }
        });
    }

    static async GetFinishedMatchesRequest() {
        return fetch('https://localhost:7241/Game/FinishedMatches', { headers : this.auth })
        .then(response => {
            if (response.status === 200)
            {
                return response.text();
            }
        });
    }

    static async GetMatchHistoryRequest(props) {
        return fetch('https://localhost:7241/Game/GetMatchHistory?' + new URLSearchParams({
            matchId: props.matchId}), {method: 'POST', headers : this.auth})
        .then(response => {
            if (response.status === 200)
            {
                return response.text();
            }
        });
    }

    static async CreateMatchRequest(props) {
        return fetch('https://localhost:7241/Game/CreateMatch?' + new URLSearchParams({
            playerId: this.userGuid}), {method: 'POST', headers : this.auth})
        .then(response => {
            if (response.status === 200)
            {
                return response.text();
            }
        });
    }

    static async JoinMatchRequest(props) {
        return fetch('https://localhost:7241/Game/JoinMatch?' + new URLSearchParams({
            matchId: props.matchId, playerId: this.userGuid}), {method: 'POST', headers : this.auth})
        .then(response => {
            if (response.status === 200)
            {
                return response.text();
            }
        });
    }

    static async CheckMatchStatus(props) {
        return fetch('https://localhost:7241/Game/MatchStatus?' + new URLSearchParams({
            matchId: this.matchId}), {method: 'POST', headers : this.auth})
        .then(response => {
            if (response.status === 200)
            {
                return response.text();
            }
        });
    }

    static async CheckTurn(props) {
        return fetch('https://localhost:7241/Game/TurnStatus?'+ new URLSearchParams({
            matchId: this.matchId, playerId: this.userGuid}), {method: 'POST', headers : this.auth})
        .then(response => {
            if (response.status === 200)
            {
                return response.text();
            }
        });
    }

    static async SendTurn(props) {
        return fetch('https://localhost:7241/Game/SendTurn?'+ new URLSearchParams({
            matchId: this.matchId, playerId: this.userGuid, x: props.x, y: props.y}), {method: 'POST', headers : this.auth})
        .then(response => {
            if (response.status === 200)
            {
                return response.text();
            }
        });
    }

    static async RefreshField(props) {
        return fetch('https://localhost:7241/Game/RefreshField?'+ new URLSearchParams({
            matchId: this.matchId}), {method: 'POST', headers : this.auth})
        .then(response => {
            if (response.status === 200)
            {
                return response.text();
            }
        });
    }

    static async GetStats(props) {
        return fetch('https://localhost:7241/Game/GetStats?'+ new URLSearchParams({
            playerId: this.userGuid}), {method: 'POST', headers : this.auth})
        .then(response => {
            if (response.status === 200)
            {
                return response.text();
            }
        });
    }
}

export {Requests}