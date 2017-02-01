import React from 'react';
import { shallow } from 'enzyme';
import { App } from 'components/App';

describe('<App />', () => {
  let wrapper;
  let getMeStub;

  beforeEach(() => {
    getMeStub = sinon.stub();
    wrapper = shallow(
      <App
        getMe={getMeStub}
        />
    );
  });

  it('should render without crashing', () => {
    expect(wrapper).to.not.be.null;
  });

  it('should invoke getMe on componentDidMount', () => {
    wrapper.instance().componentDidMount();

    expect(getMeStub).to.have.been.calledOnce;
  });
});
