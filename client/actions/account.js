export const SIGNOUT_SUCCEEDED = 'SIGNOUT_SUCCEEDED';
export const SIGN_IN_SUCCEEDED = 'SIGN_IN_SUCCEEDED';

export function onSignOut() {
  return {
    type: SIGNOUT_SUCCEEDED,
  };
}

export function onSignIn(userProfile) {
  return {
    type: SIGN_IN_SUCCEEDED,
    userProfile,
  };
}
