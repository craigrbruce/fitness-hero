import { noop } from 'lodash';
import Oidc from 'oidc-client';
import { onSignIn } from '../actions/account';

const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:5000' : 'https://dev.fitnesshero.co'; // TODO obviously more sophisticated env config required

const config = {
  authority: baseUrl,
  client_id: 'js',
  redirect_uri: `${baseUrl}/`,
  response_type: 'id_token token',
  scope: 'openid profile api1',
  post_logout_redirect_uri: `${baseUrl}/`,
};

class Auth {
  constructor(store) {
    this.store = store;
    this.mgr = new Oidc.UserManager(config);
  }

  registser = () => noop; // TODO how the f do we register from here?

  signIn = () => this.mgr.signinRedirect();

  signOut = () => {
    localStorage.removeItem('access_token');
    this.mgr.signoutRedirect();
  }

  getUser = () => (
    this.mgr.getUser()
      .then((user) => {
        if (!user) {
          return Promise.reject();
        }
        localStorage.setItem('access_token', user.access_token);
        this.store.dispatch(onSignIn(user.profile));
        return Promise.resolve();
      })
  );
}

export default Auth;
