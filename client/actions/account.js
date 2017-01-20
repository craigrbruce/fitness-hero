export const SIGNOUT_SUCCEEDED = 'SIGNOUT_SUCCEEDED';
export const SIGNIN_SUCCEEDED = 'SIGNIN_SUCCEEDED';

export function onSignOut() {
  return {
    type: SIGNOUT_SUCCEEDED,
  };
}

export function onSignIn(userProfile, token) {
  return {
    type: SIGNIN_SUCCEEDED,
    userProfile,
    token,
  };
}
