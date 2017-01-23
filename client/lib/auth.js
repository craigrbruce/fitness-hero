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

  signIn = () => this.mgr.signinRedirect();

  signOut = () => {
    // TODO .. dispatch action to clear state token
    this.mgr.signoutRedirect();
  }

  getUser = () => (
    this.mgr.getUser()
      .then((user) => {
        if (!user) {
          return Promise.reject();
        }
        this.store.dispatch(onSignIn(user.profile));
        return Promise.resolve();
      })
  );
}

export default Auth;
