using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public int maxHealth = 100;
    public int totalNumberOfSteps = 6;

    [Header("Self-component references")]
    [SerializeField] Transform[] stepsParents;
    
    [Header("External component references")]
    [SerializeField] HealthBar healthBar;
    
    private int _playerHealth;
    private int _currentStep;
    private float _changeOffset;

    private int _previousStep;
    
    void Start()
    {
        _playerHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        _changeOffset = maxHealth / (float) totalNumberOfSteps; // = 16.67
        // if we do not cast (float) it will ignore the floating point ".67"
        // _changeOffset = maxHealth / totalNumberOfSteps; // = 16

        _previousStep = totalNumberOfSteps;
    }
    
    public void TakeDamage(int damageAmount)
    {
        _playerHealth -= damageAmount;
        healthBar.SetHealth(_playerHealth);

        if (_playerHealth <= 0)
        {
            GameManager.Instance.SetGameOver();
            return;
        }

        int nextStep = Mathf.RoundToInt(_playerHealth / _changeOffset);
        Debug.Log($"nextStep {nextStep} , previousStep {_previousStep}");
        if (nextStep != _previousStep)
        {
            DamageTowerStep();
            _previousStep = nextStep;
        }
    }

    private void DamageTowerStep()
    {
        for (int i = 0; i < stepsParents.Length; i++)
        {
            stepsParents[i].GetChild(_currentStep).gameObject.SetActive(false);
        }
        _currentStep++;
    }
}
