import Oidc from 'oidc-client';
import { onSignIn } from '../actions/account';

const config = {
  authority: 'http://localhost:5000',
  client_id: 'js',
  redirect_uri: 'http://localhost:5003/callback.html',
  response_type: 'id_token token',
  scope: 'openid profile api1',
  post_logout_redirect_uri: 'http://localhost:5003/index.html',
};

if (typeof location.origin === 'undefined') {
  location.origin = `${location.protocol}//${location.host}`;
}

class Auth {
  constructor(store) {
    this.store = store;
    this.mgr = new Oidc.UserManager(config);
  }

  signIn = () => this.mgr.signinRedirect();

  signOut = () => {
    localStorage.removeItem('access_token');
    this.mgr.signoutRedirect();
  }

  getUser = () => (
    this.mgr.getUser()
      .then((user) => {
        localStorage.setItem('access_token', user.access_token);
        this.store.dispatch(onSignIn(user.profile));
      })
  );
}

export default Auth;
